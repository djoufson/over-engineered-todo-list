<script setup lang="ts">
import { getAllTodos } from '@/services/api';
import type { Todo } from '@/types/Todo';
import TodoItem from '@/components/TodoItem.vue'
import type { PagedList } from '@/types/pagedList';
import { ref } from 'vue';
import Pagination from '@/components/Pagination.vue';

const page = ref(1);
const size = ref(5);
const todos: PagedList<Todo> = await getAllTodos(page.value, size.value);
</script>

<template>
  <div class="w-full flex flex-col gap-3 mt-8">
    <TodoItem />
    <div v-for="todo in todos.items" :key="todo.id" class="w-full">
      <TodoItem :todo="todo" />
    </div>

    <Pagination
      :current-page="todos.page"
      :current-size="todos.size"
      :has-next-page="todos.hasNextPage"
      :has-previous-page="todos.hasPreviousPage"
      :total-count="todos.totalCount"
      :total-pages="todos.totalPages"/>
  </div>
</template>
