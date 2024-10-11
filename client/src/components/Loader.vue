<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import { LottieAnimation } from "lottie-web-vue"
  import LoaderJson from "../../public/animations/loader-animation.json"

  let anim = ref()

  onMounted(() => {
    setTimeout(() => {
      console.log(anim.value.goToAndPlay(150, true))
      anim.value
    }, 500)
  })
</script>
<template>
  <LottieAnimation 
    :animation-data="LoaderJson"
    :auto-play="true"
    :loop="true"
    :speed="1"
    ref="anim" />
</template>