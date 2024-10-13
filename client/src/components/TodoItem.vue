<script setup lang="ts">
import { createTodo, deleteTodo, getTags, unassignTag, updateTodo } from '@/services/api';
import type { Todo } from '@/types/Todo';
import { emptyTodo } from '@/utilities/todoUtilities';
import { formatDate } from '@/utilities/dateUtilities';
import { Icon } from '@iconify/vue';
import { ref } from 'vue';

const props = defineProps<{
  todo?: Todo,
  onTodoDeleted: (id:string) => void,
  onTodoCreated: (todo:Todo) => void,
}>()

const isFocus = ref(false);
const deletedClass = ref("");
const tagsToggleClass = ref("");
const popupToggledClass = ref("");
const filterTag = ref("");
const model = props.todo === undefined || props.todo === null ? ref(emptyTodo()) : ref(props.todo);
const filteredTags = ref(await getTags(filterTag.value, model.value.tags));

async function onSave() {
  if(props.todo === undefined || props.todo === null) {
    if(model.value.title) {
      console.log(model.value.title);
      const createdTodo = await createTodo(model.value);
      if(createdTodo) {
        model.value = emptyTodo();
        props.onTodoCreated(createdTodo);
      }
    }
    else {
      alert("The task title is required");
    }
  }
  else {
    updateTodo(model.value);
  }
}

async function onDelete() {
  const success = await deleteTodo(props.todo?.id ?? "");
  if(success) {
    deletedClass.value = "deleted";
    props.onTodoDeleted(props.todo?.id ?? '');
  }
}

async function onTagFiltered() {
  filteredTags.value = await getTags(filterTag.value, model.value.tags);
}

function onFocusOut() {
  if(props.todo != undefined && props.todo != null) {
    updateTodo(model.value);
  }
}

function toggleTags() {
  if(tagsToggleClass.value === "rotate-180") {
    hideTagsPopup();
  }
  else {
    showTagsPopup();
  }
}

function showTagsPopup() {
  tagsToggleClass.value = "rotate-180";
  popupToggledClass.value = "toggled";
}

function hideTagsPopup() {
  tagsToggleClass.value = "rotate-0";
  popupToggledClass.value = "";
}

function onLoseHover() {
  hideTagsPopup();
}

async function removeTag(tag:string) {
  if(props.todo != null && props.todo != undefined) {
    const success = await unassignTag(model.value.id, tag);
    if(!success) {
      return;
    }
  }
  model.value.tags = model.value.tags.filter(t => t != tag);
}

</script>

<template>
  <div :class="`todo-item ${deletedClass} transition-all relative`" @mouseleave="onLoseHover">
    <div class="flex gap-[2px] w-full border-2 border-blue-300 rounded-lg p-2 items-center">
      <input type="text" placeholder="Add a new task" @keydown.enter="onSave" @focusin="isFocus = true"
        @focusout="onFocusOut" v-model="model.title" class="bg-transparent p-2 w-full focus:outline-none">
      <div v-if="props.todo" class="todo-action flex items-center justify-center">
        <button @click="onDelete"
          class="w-[30px] flex items-center justify-center text-red-300 hover:text-red-400 transition-all">
          <Icon width="100%" icon="mdi-light:delete" />
        </button>
      </div>
      <div class="todo-action flex items-center justify-center">
        <button
          @click="toggleTags"
          class="relative w-[30px] flex items-center justify-center text-blue-300 hover:text-blue-400 transition-all">
          <Icon width="100%" icon="mdi-light:chevron-down" :class="`${tagsToggleClass} transition-all`" />
        </button>
      </div>
    </div>
    <div class="flex justify-between px-2 mt-1 gap-4">
      <div class="flex gap-1 flex-wrap cursor-pointer">
        <button @click="removeTag(tag)" v-for="tag in model.tags" class="flex gap-1 items-center text-xs p-1 bg-orange-300">
          {{ tag }}
          <Icon icon="mdi-light:cancel" />
        </button>
      </div>
      <span v-if="props.todo" class="text-xs">{{ formatDate(model.dueDate ?? new Date()) }}</span>
    </div>
    <!-- Tags popup -->
    <div :class="`${popupToggledClass} tags-popup flex flex-col gap-2 absolute right-0 top-[60px] min-w-[33%] max-w-[50%] w-full p-4 rounded-lg shadow bg-white transition-all`">
      <input type="text" placeholder="Add a tag" v-model="filterTag" @input="onTagFiltered" class="w-full text-sm">
      <div>
        <button v-for="tag in filteredTags" class="flex gap-1 items-center text-xs p-1 bg-orange-300">
          {{ tag.name }}
        </button>
      </div>
    </div>
  </div>
  <span class="rotate-180 hidden"></span>
  <span class="rotate-0 hidden"></span>
</template>
