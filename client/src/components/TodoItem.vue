<script setup lang="ts">
import { createTodo, updateTodo } from '@/services/api';
import type { Todo } from '@/types/Todo';
import { emptyTodo } from '@/utilities/todoUtilities';
import { ref } from 'vue';

const props = defineProps<{
  todo?: Todo
}>()

const isFocus = ref(false);
const model = props.todo ? ref(emptyTodo()) : ref(structuredClone(props.todo));

function onSave() {
  if(props.todo) {
    updateTodo(model.value as Todo);
  }
  else {
    createTodo(model.value as Todo);
  }
}

</script>

<template>
  <div class="flex gap-4 w-full border-2 border-blue-200 rounded-lg p-2 items-center">
    <input @keydown.enter="onSave" type="text" placeholder="Add a new task" @focusin="isFocus = true" @focusout="isFocus = false" :value="todo?.title" class="bg-transparent p-2 w-full focus:outline-none">
    <div v-if="isFocus">
      <div v-if="todo">
        <button>Save</button>
      </div>
      <div v-else>
        <button>Add</button>
      </div>
    </div>
  </div>
</template>
