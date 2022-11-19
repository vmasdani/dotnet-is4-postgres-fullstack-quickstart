<script setup lang="ts">
import { ref } from "vue";
import { ISTokenEndpointReturn } from "@/protos/dotnetapp";
import { ctx } from "./main";

const username = ref("");
const password = ref("");

const handleLogin = async () => {
  try {
    const resp = await fetch(`${window.location.origin}/login`, {
      method: "post",
      headers: { "content-type": "application/json" },
      body: JSON.stringify({
        username: username.value,
        password: password.value,
      }),
    });

    if (resp.status !== 200) throw await resp.text();

    ctx.value.identityServerKey = ISTokenEndpointReturn.fromPartial(
      await resp.json()
    );
  } catch (e) {
    console.error(e);
    alert(`Login failed: ${e}`);
  }
};
</script>
<template>
  <div class="vh-100 vw-100 d-flex justify-content-center align-items-center">
    <form
      @submit="
        (e) => {
          e.preventDefault();
          handleLogin();
        }
      "
    >
      <div class="p-3 border border-dark shadow shadow-md">
        <div>
          <input
            placeholder="Username..."
            class="form-control form-control-sm"
            @input="(e) => {
            username = (e.target as HTMLInputElement).value
          }"
          />
        </div>
        <div>
          <input
            placeholder="Password..."
            class="form-control form-control-sm"
            @input="(e) => {
            password = (e.target as HTMLInputElement).value
          }"
          />
        </div>
        <div>
          <button type="submit" class="btn btn-sm btn-primary">Login</button>
        </div>
      </div>
    </form>
  </div>
</template>
