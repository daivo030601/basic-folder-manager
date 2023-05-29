<template>
  <div
    id="authentication-modal"
    tabindex="-1"
    aria-hidden="true"
    :class="{ hide: isEntryHide }"
    class="fixed top-0 left-0 right-0 z-50 w-full p-4 overflow-x-hidden overflow-y-auto md:inset-0 h-modal md:h-full modal-folder"
  >
    <div
      class="relative w-full h-full max-w-3xl md:h-auto"
      :style="{ top: 10 + 'vh', left: 29 + 'vw' }"
    >
      <div class="b-modal">
        <div class="b-modal__header">
          <div class="fg1">
            <div class="flex justify-between">
              <h2 class="font-semibold b-title">Create entry</h2>
              <button
                type="button"
                @click="toggleEntryModal"
                class="absolute top-3 right-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
                data-modal-hide="authentication-modal"
              >
                <svg
                  aria-hidden="true"
                  class="w-5 h-5"
                  fill="currentColor"
                  viewBox="0 0 20 20"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    fill-rule="evenodd"
                    d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                    clip-rule="evenodd"
                  ></path>
                </svg>
                <span class="sr-only">Close modal</span>
              </button>
            </div>
          </div>
        </div>
        <div class="b-modal__body b-modal__body--scrollable fg1 h400">
          <div class="b-modal__scrollable__wrapper">
            <div
              v-for="entry in entries"
              :key="entry.id"
              class="b-card rounded-2xl shadow-lg"
            >
              <div class="b-card__body">
                <div class="r">
                  <div class="c12">
                    <button
                      type="button"
                      @click="delRow(entry.id)"
                      class="absolute right-2.5 bottom-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 inline-flex items-center dark:hover:bg-gray-800 dark:hover:text-white"
                      data-modal-hide="authentication-modal"
                    >
                      <svg
                        aria-hidden="true"
                        class="w-5 h-5"
                        fill="currentColor"
                        viewBox="0 0 20 20"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          fill-rule="evenodd"
                          d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                          clip-rule="evenodd"
                        ></path>
                      </svg>
                      <span class="sr-only">Close modal</span>
                    </button>
                    <div class="form-item rounded-2xl">
                      <label class="form-item__label">
                        <span>Question?</span>
                      </label>
                      <div class="b-input">
                        <div class="flex-grow-1 relative">
                          <input
                            v-model="entry.description"
                            type="text"
                            autocomplete="off"
                            placeholder="Give the question here..."
                            class="b-input__inner"
                            fdprocessedid="ulryzn"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="c12">
                    <div class="mt-4">
                      <label class="form-item__label"
                        ><span>Response</span></label
                      >
                      <main>
                        <textarea
                          v-model="entry.answer"
                          id="message"
                          rows="4"
                          class="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                          placeholder="Write your thoughts here..."
                        ></textarea>
                      </main>
                    </div>
                  </div>
                  <div class="c12">
                    <div class="mt-4">
                      <label class="form-item__label"
                        ><span>Directory</span>
                        <!----></label
                      >
                      <div class="b-cascader">
                        <div class="b-input cursor-pointer overflow-hidden">
                          <div class="relative flex-grow-1">
                            <input
                              v-model="entry.folderId"
                              type="number"
                              autocomplete="off"
                              placeholder="Please select a parent"
                              class="b-input__inner"
                              fdprocessedid="itfrb4"
                              aria-expanded="false"
                            />
                          </div>
                          <div class="b-input__suffix">
                            <a target="_blank"
                              ><font-awesome-icon :icon="['fas', 'arrow-down']"
                            /></a>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="b-modal__footer">
          <div class="flex mt-6 justify-end">
            <button
              @click="newRow"
              type="button"
              class="b-btn b-btn--primary medium mr-2"
            >
              Add entry
            </button>

            <button
              @click="show"
              type="button"
              class="b-btn b-btn--primary medium"
            >
              Create
            </button>
          </div>
        </div>
      </div>
      <!-- Modal content -->
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, Property } from "vue";
import axios from "axios";
import Vue from "vue";
import { booleanLiteral } from "@babel/types";
import { randomInt } from "crypto";

interface IEntry {
  id: number;
  description?: string;
  answer?: string;
  folderId?: number;
}

const props = defineProps({
  isEntryHide: {
    type: Boolean,
  },
  toggleEntryModal: {
    type: Function,
  } as Property<Function>
})

const entries = reactive([]) as IEntry[]

function newRow() {
  console.log(toRaw(entries));
  entries.push({id: Math.floor(Math.random() * 2147483647 )});
}

const delRow = (id: number) => {
  const index = entries.findIndex((x) => x.id == id);
  if (index > -1) {
    entries.splice(index, 1);
  }
}

 onMounted(() => {
  entries.push({ id: Math.floor(Math.random() * 2147483647) });
})



async function show() {
  try {
    await axios
      .post("https://localhost:7145/api/Library/Questions",
        toRaw(entries)
      )
      .then((response) => {
        console.log(response);
        props.toggleEntryModal();
      });
  } catch (error) {
    console.log(error);
  }
}


</script>

<style lang="scss" scoped>
@import "./_entryModal.scss";
</style>
