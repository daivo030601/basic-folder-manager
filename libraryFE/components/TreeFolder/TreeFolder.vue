<template>
  <div class="b-tree-node relative">
    <div class="absolute id-tag">
      <span>{{props.id}}</span>
    </div>
    <div class="b-tree-node__content"
      @click="onClickFolder()" 
      :style="{ paddingLeft: props.depth * 16 + 'px' }
    ">
      <a target="_blank" class="expand" :class="{expanded: state.expanded, focused: state.focused}" ><font-awesome-icon :icon="['fas', 'angle-right']" v-show="props.children.length > 0"/></a>
      <div class="flex mwx240">
        <span class="tuels">{{ props.name }}</span>
      </div>
    </div>
    <div 
      class="b-tree-node__children"
      v-show="state.showChildren"
    >
      <TreeFolder
        v-for="(item,index) in props.children"
        :key="index"
        :name="item.name"
        :id="item.id"
        :children="item.children"
        :depth="props.depth + 1"
        @onClickFolder="onClickFolderChild($event)"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref,defineProps } from 'vue';
  const state = reactive({ 
    showChildren: false,
    expanded: false,
    focused: false
  })
  const emits = defineEmits(['onClickFolder'])
  const props = defineProps({
    id: {
      type: Number,
    },
    name: {
      type: String,
    },
    depth: {
      type: Number,
    },
    children: {
      type: Array,
    }
  })


  function onClickFolder() {
    console.log('aloooo', state.showChildren);
    
    emits("onClickFolder", props.id)
    state.showChildren = !state.showChildren;
    state.expanded = !state.expanded;
  }

  const onClickFolderChild = (id: any) => {
    emits('onClickFolder', id);
  }

</script>

<style lang="scss" scoped>
  @import './_treeFolder.scss';
</style>