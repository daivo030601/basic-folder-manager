export default defineNuxtRouteMiddleware((to, from) => {
  if (to.fullPath === "/library") {
    if(!localStorage.getItem("token")){
      console.log("aloooooooo",to);
      return navigateTo('/')
    }
  }
})