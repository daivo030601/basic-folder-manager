

<template>
  <div class="h-screen">
    <HeadBar/>
    <div class="flex h-4/6 items-center justify-center">
      <div class="w-full max-w-sm space-y-8">
        <div>
          <h2 class="mt-6 text-center text-3xl font-bold tracking-tight text-gray-900">LOGIN</h2>
          
        </div>
        <form class="mt-8 space-y-6" action="#" method="POST">
          <div class="space-y-4 rounded-md shadow-sm">
            <div>
              <label for="email-address" class="sr-only">Email address</label>
              <input v-model="state.email" id="email-address" name="email" type="email" required class="relative block w-full appearance-none rounded-sm border border-gray-300 px-3 py-3 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 sm:text-md" placeholder="Email address">
            </div>
            <div class="mt-6">
              <label for="password" class="sr-only">Password</label>
              <input v-model="state.password" id="password" name="password" type="password" required class="relative block w-full appearance-none rounded-sm border border-gray-300 px-3 py-3 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 sm:text-md" placeholder="Password">
            </div>
          </div>

          <div class="flex-center">
            <div class="flex items-center justify-center">
              <input id="remember-me" name="remember-me" type="checkbox" class="h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
              <label for="remember-me" class="ml-2 block text-sm text-gray-900">Remember me</label>
            </div>
          </div>

          <div>
            <button @click="onSubmit" type="button" class="bg-[#15113f] group relative inset-0 m-auto flex w-fit justify-center self-center rounded-3xl border border-transparent bg-indigo-600 py-3 px-6 text-sm font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2">
              Login
            </button>
          </div>
          <div class="text-sm text-center">
              <a href="#" class="font-medium text-indigo-600 hover:text-indigo-500">Forgot password?</a>
            </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import HeadBar from "../components/HeadBar/HeadBar.vue"
  import axios from "axios"
  const router = useRouter()
  const state = reactive({
    email: '',
    password: '',
    error: ''
  })

  function checkUser() {
    if (localStorage.getItem("token")) 
    {
      router.push({
              path: '/library'
          })
    }
  }

  async function onSubmit() {
      try {
        await axios
          .post("https://localhost:7145/api/Auth/SignIn",{
            email: state.email,
            password: state.password
          })
          .then((response) => {
            if(response.data !== "Login fail") {
              if(process.client) {
                console.log(response.data)
                localStorage.setItem("token", response.data)
              }
              router.push({
                  path: '/library'
              })
            } else {
              state.error = "something went wrong, check your email and password"
            }
          });
      } catch (error) {
        console.log(error);
      }
    }
  
  definePageMeta({
    middleware: ["check-user"]
    // or middleware: 'auth'
  })

</script>