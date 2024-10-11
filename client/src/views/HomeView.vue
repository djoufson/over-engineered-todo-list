<script setup lang="ts">
import AllTodos from '@/components/AllTodos.vue'
import Loader from '@/components/Loader.vue';

</script>

<template>
  <div class="max-w-[500px] w-full mx-auto p-8">
    <h1 class="text-4xl font-bold text-center">
      Over-engineered Todo List!
    </h1>
    <Suspense>
      <AllTodos />
      <template #fallback>
        <Loader />
      </template>
    </Suspense>
  </div>
</template>
