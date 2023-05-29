<template>
  <div class="sideNav py-4 h-screen w84">
    <div class="contentNav shadow-2xl h-full flex flex-col">
      <div class="logo py-5 mb-3.5 px-3">
        <div class="ovh">
          <img src="https://admin.afp-qa.dc.bigin.top/_nuxt/img/Logo_Full.9575890.svg" class="img h-11"/>
        </div>
      </div>
      <div class="content flex justify-between flex-col">
        <div class="wrap px-0.5">
          <ul>
            <li>
              <a target="_blank" class="icon p-1 rounded-xl"><font-awesome-icon :icon="['fas', 'house']" /></a>
            </li>
            <li>
              <a target="_blank" class="icon p-1 rounded-xl"><font-awesome-icon :icon="['far', 'folder']" /></a>
            </li>
            <li>
              <a target="_blank" class="icon p-1 rounded-xl"><font-awesome-icon :icon="['far', 'file']" /></a>
            </li>
          </ul>
        </div>
        <div class="footer">
          <a target="_blank" @click="logout" class="icon p-1 rounded-xl"><font-awesome-icon :icon="['fas', 'arrow-right-from-bracket']" /></a>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const route = useRouter();
function logout() {
  localStorage.clear();
  route.push({
            path: '/'
        })
}
  
</script>


<style lang="scss" scoped>
  @import './_sideNavBar.scss';
</style>