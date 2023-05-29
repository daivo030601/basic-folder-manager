// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  css: [
    '~/assets/css/main.css',
    '@fortawesome/fontawesome-svg-core/styles.css'
  ],
  ssr: false,
  
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },
})
