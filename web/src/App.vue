<script setup>
import { ref } from "vue";
const imgSrc = ref("");
const handleFileChange = (e) => {
  const file = e.target.files[0];
  const reader = new FileReader();
  reader.onload = () => {
    imgSrc.value = reader.result;
  };
  reader.readAsDataURL(file);
};
const handleClick = () => {
  console.log(imgSrc.value);
};
</script>

<template>
  <div>
    <input type="file" @change="handleFileChange" />
    <button @click="handleClick">upload</button>
    <img :src="imgSrc" />
  </div>
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
