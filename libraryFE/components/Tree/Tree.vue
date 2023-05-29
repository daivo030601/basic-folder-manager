<template>
  <div class="b-tree mt-6 mx-3">
    <TreeFolder
      v-for="(item, index) in props.menuFolders"
      :key="index"
      :name="item.name"
      :id="item.id"
      :children="item.children"
      :depth="0"
      @onClickFolder="onClickFolder($event)"
    />
  </div>
</template>

<script setup lang="ts">
import { toRefs, defineProps } from 'vue';
  const TreeFolder = resolveComponent('TreeFolder')
  const props = defineProps({
    menuFolders: {
      type: [Array],
    }
  })
  const emits = defineEmits(['onClickFolder'])

  function onClickFolder(id: any) {
    emits('onClickFolder', id);
  }
 

</script>

<style lang="scss" scoped>
  @import './_tree.scss';
</style>
