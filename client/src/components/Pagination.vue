<script setup lang="ts">
const props = defineProps<{
  currentPage: number,
  currentSize: number,
  totalCount: number,
  hasPreviousPage: boolean,
  hasNextPage: boolean,
  totalPages: number
}>();

</script>

<template>
  <div class="flex w-full justify-between mt-20">
    <span v-if="hasPreviousPage"><a>< {{ currentPage - 1 }}</a></span>
    <span v-if="hasNextPage || hasPreviousPage" class="text-center">page {{ currentPage }} of {{ totalPages }}</span>
    <span v-if="hasNextPage"><a>{{ currentPage + 1 }} ></a></span>
  </div>
</template>
