<script setup lang="ts">
import TodoItem from '@/components/TodoItem.vue'
import { getAllTodos } from '@/services/api';
import type { Todo } from '@/types/Todo';

const todos: Todo[] = await getAllTodos();

</script>

<template>
  <div class="max-w-[500px] w-full mx-auto p-8">
    <h1 class="text-4xl font-bold text-center">
      Over-engineered Todo List!
    </h1>
    <div class="w-full flex flex-col gap-3 mt-8">
      <TodoItem />
      <div v-for="todo in todos" :key="todo.id" class="w-full">
        <TodoItem :todo="todo" />
      </div>
    </div>
  </div>
</template>
