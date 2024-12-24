<script setup>
import { ref } from "vue";
import * as wasm from "./assets/wasm/main";

const imgSrc = ref("");
const decodeRes = ref();
const handleFileChange = (e) => {
  const file = e.target.files[0];
  const reader = new FileReader();
  reader.onload = () => {
    imgSrc.value = reader.result;
  };
  reader.readAsDataURL(file);
};
const handleClick = async () => {
  console.log(imgSrc.value);
  const res = await wasm.decodeImage(imgSrc.value.split(",")[1]);
  decodeRes.value = JSON.parse(res);
};
</script>

<template>
  <div>
    <input type="file" @change="handleFileChange" />
    <button @click="handleClick">decode</button>
    <div class="flex">
      <img :src="imgSrc" class="preview" />
      <div>
        <div v-if="decodeRes && decodeRes.length == 0">没有识别到条形码</div>
        <div v-for="(item, index) in decodeRes">
          <div>结果{{ index + 1 }}:{{ item }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.flex{
  display: flex;
}
.preview {
  width: 300px;
}
</style>
