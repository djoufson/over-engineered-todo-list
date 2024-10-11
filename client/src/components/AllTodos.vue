<script setup lang="ts">
import { getAllTodos } from '@/services/api';
import type { Todo } from '@/types/Todo';
import TodoItem from '@/components/TodoItem.vue'

const todos: Todo[] = await getAllTodos();
</script>

<template>
  <div class="w-full flex flex-col gap-3 mt-8">
    <TodoItem />
    <div v-for="todo in todos" :key="todo.id" class="w-full">
      <TodoItem :todo="todo" />
    </div>
  </div>
</template>
