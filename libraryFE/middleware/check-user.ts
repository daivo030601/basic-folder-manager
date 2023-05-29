export default defineNuxtRouteMiddleware((to, from) => {
  if (to.fullPath === "/") {
    if(localStorage.getItem("token")){
      console.log("aloooooooo",to);
      return navigateTo('/library')
    }
  }
})