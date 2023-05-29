<template>
  <main class="mb-4 flex wrap-item">
    <div class="w-full">
      <div class="flex">
        <div class="flex items-center">
          <span class="font-semibold cursor-pointer fsxs">Entire Library</span>
        </div>
      </div>
      <div class="flex my-2">
        <span class="cursor-pointer fsxl tuels font-semibold mx-1 ">{{props.description}}</span>
      </div>
      <div class="p-4 relative answer">
        <div class="c12 mb-1 flex justity-between items-center">
          <div class=""></div>
        </div>
        <span class="c12 w-full mt-1 rxs response-collapse">
          <p>{{props.answer}}</p>
        </span>
      </div>
    </div>
  </main>
</template>

<script setup lang="ts">
  const props = defineProps({
    answer: {
      type: [String],
    },
    description: {
      type: [String],
    },
    id: {
      type: [Number],
    }
  })
  
</script>

<style lang="scss" scoped>
  @import './_entryItem.scss';
</style>