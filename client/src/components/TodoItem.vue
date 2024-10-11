<script setup lang="ts">
import { createTodo, deleteTodo, updateTodo } from '@/services/api';
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

const model = props.todo === undefined || props.todo === null ? ref(emptyTodo()) : ref(props.todo);

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

function onFocusOut() {
  if(props.todo != undefined && props.todo != null) {
    updateTodo(model.value);
  }
}

</script>

<template>
  <div :class="`todo-item ${deletedClass} transition-all`">
    <div class="flex gap-4 w-full border-2 border-blue-200 rounded-lg p-2 items-center">
      <input
        type="text"
        placeholder="Add a new task"
        @keydown.enter="onSave"
        @focusin="isFocus = true"
        @focusout="onFocusOut"
        v-model="model.title"
        class="bg-transparent p-2 w-full focus:outline-none">
      <div
        v-if="props.todo"
        class="todo-action w-[50px] flex items-center justify-center">
        <button
          @click="onDelete"
          class="w-full flex items-center justify-center text-red-300 hover:text-red-400 transition-all">
          <Icon
            width="60%"
            icon="mdi-light:delete"/>
        </button>
      </div>
    </div>
    <span v-if="props.todo" class="pl-3 text-xs">{{ formatDate(model.dueDate ?? new Date()) }}</span>
  </div>
</template>
