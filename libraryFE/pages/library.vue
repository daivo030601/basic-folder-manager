

<template>
  <div class="app-container">
    <div class="df">
      <div class="appNavigation">
        <SideNavBar/>
      </div>
      <div class="page-container">
        <div class="title relative items-center ">
          <div class="col">
            <h1>Library management</h1>
          </div>
        </div>
        <div class="fg1 pt6x">
          <main class="h-full">
            <div class="row h-full">
              <div class="h-full col-3">
                <div class="h-full fg1 relative flex flex-col rounded-2xl b-card shadow-lg">
                  <div class="p6x flex fg1 flex-col">
                    <div class="h-full flex flex-col">
                      <div class="flex items-center justify-between">
                        <h2>Explorer</h2>
                        <button class="btn-icon" id="button" @click="toggleModal">
                          <a target="_blank"><font-awesome-icon :icon="['fas', 'plus']" /></a>
                        </button>
                      </div>
                      <Loading v-if="state.isFolderLoading"/>
                      <Tree
                        :menuFolders="state.treeFolder"
                        @onClickFolder="getFolderFocused($event)"
                      />
                      <span class="create-btn" @click="toggleModal">
                        <a target="_blank" class="b-icon icsz-14 cp3"><font-awesome-icon :icon="['fas', 'plus']" /></a>
                        <span class="cp3 mx-1 font-semibold">Create folder</span>
                      </span>
                    </div>
                  </div>
                </div>
              </div>
              <div class="h-full col">
                <div class="h-full fg1 flex relative flex-col b-card shadow-xl rounded-2xl">
                  <div class="b-card__header p6x shadow-md">
                    <div>
                      <div class="df">
                        <form class="w-full mb-4">
                          <div class="b-input rounded-full">
                            <div class="relative flex-grow-1">
                              <input 
                                v-model="state.searchString"
                                type="text" 
                                autocomplete="off" 
                                placeholder="Searching for..." 
                                class="b-input__inner font-semibold"
                              />
                            </div>
                            <div class="b-input--suffix"></div>
                            <div class="b-input-group__append">
                              <button type="button" class="btn-search b-btn-icon rounded-r-full" @click="onClickSearch">
                                <a target="_blank" class=""><font-awesome-icon :icon="['fas', 'magnifying-glass']" /></a>
                              </button>
                            </div>
                          </div>
                        </form>
                      </div>
                      <div class="flex flex-between flex-wrap rxs">
                        <div class="c left">
                          <button @click="toggleEntryModal" class="b-btn btn-create font-semibold text-lg">
                            <a target="_blank" class="b-icon font-normal mr-1"><font-awesome-icon :icon="['fas', 'plus']" /></a>  
                            New entry
                          </button>
                        </div>
                        <div class="ca right">
                          <div class="flex items-center ">
                            <button class="tag-btn b-btn small font-semibold gap-1">
                              <a target="_blank" class="b-icon font-normal mr-1"><font-awesome-icon :icon="['fas', 'tag']" /></a>  
                              All tags
                              <a target="_blank" class="b-icon font-normal mr-1"><font-awesome-icon :icon="['fas', 'angle-down']" /></a>
                            </button>
                            <div class="mx-2 relative inline-flex">
                              <button class="tag-btn b-btn small font-semibold gap-1">
                                <a target="_blank" class="b-icon font-normal mr-1"><font-awesome-icon :icon="['fas', 'arrow-up-wide-short']" /></a>
                                Sort
                                <a target="_blank" class="b-icon font-normal mr-1"><font-awesome-icon :icon="['fas', 'angle-down']" /></a>
                              </button>
                            </div>
                            <label class="relative inline-flex items-center cursor-pointer">
                              <input type="checkbox" value="" class="sr-only peer">
                              <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-violet-600"></div>
                              <span class="b-switch__label font-semibold mr-2 inline-flex items-center ml-1.5">Has attachments</span>
                            </label>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="b-card__body px-0 pt-0 flex flex-col p6x fg1">
                    <div class="h-full flex flex-col">
                      <div class="mt-2 flex fg1 flex-col">
                        <div class="h-full b-scrollbar">
                          <div class="b-scrollbar__wrap" >
                            <div class="b-scrollbar__view">
                              <Loading v-if="state.isEntryLoading"/>
                              <EntryItem
                                v-if="state.entries"
                                v-for="(item, index) in state.entries"
                                :key = "index"
                                :description = "item.description"
                                :answer = "item.answer"
                                :id="item.id"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <main>
              <EntryModal
                :isEntryHide="state.isEntryHide"
                :toggleEntryModal="toggleEntryModal"
              />
            </main>
          </main>
          <Modal
            :focused="state.folderFocused" 
            :isHide="state.isHide"
            :toggleModal="toggleModal"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
//@ts-ignore
  import SideNavBar from "../components/SideNavBar/SideNavBar.vue"
  import Tree from "~~/components/Tree/Tree.vue";
  import { onMounted } from 'vue'
  import Modal from '~~/components/Modal/Modal.vue'
  import EntryItem from '~~/components/EntryItem/EntryItem.vue'
  import EntryModal from '~~/components/EntryModal/EntryModal.vue'
  import Loading from '~~/components/Loading/Loading.vue'
  import axios from 'axios'

  const router = useRouter()
  const state = reactive({
    isHide: true,
    isEntryHide: true,
    isFolderLoading: true,
    isEntryLoading: true,
    searchString: "",
    folderFocused: 1,
    entries: [],
    treeFolder: [],
  })
  function toggleModal() {
    state.isHide = !state.isHide;
  }

  function toggleEntryModal() {
    state.isEntryHide = !state.isEntryHide;
  }

 
  async function onClickSearch() {
    state.isEntryLoading = true;
    if (state.searchString) {
      await fetchEntrySearch();
    } else {
      await fetchEntry();
    }
    
  }

  async function fetchEntry() {
    const entryResponse = await axios.get(`https://localhost:7145/api/Library/Questions/${state.folderFocused}`)
    state.entries = entryResponse.data.responseData;
    state.isEntryLoading = false;
  }

  async function fetchTreeFolder() {
    const treeFolderResponse = await axios.get('https://localhost:7145/api/Library/AllFolders')
    state.treeFolder = treeFolderResponse.data.responseData;
    state.isFolderLoading = false;
  }

  async function fetchEntrySearch() {
    const entryResponse = await axios.get(`https://localhost:7145/api/Library/Question/${state.searchString}/${state.folderFocused}`)
    state.entries = entryResponse.data.responseData;
    state.isEntryLoading = false;
  }

  async function getFolderFocused(id: any) {
    state.isEntryLoading = true;
    state.folderFocused = id
    console.log("focused", state.folderFocused);
    await fetchEntry()

  }

  
  definePageMeta({
    middleware: ["auth"]
    // or middleware: 'auth'
  })

  onMounted(async () => {
    await fetchTreeFolder()
    await fetchEntry()
  })
  
</script>

<style lang="scss" scoped>
  @import '../assets/scss/library.scss';
</style>