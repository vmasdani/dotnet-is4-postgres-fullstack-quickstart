<script setup lang="ts">
import { ref } from "vue";
import { fetchUserInfoProto } from "./helpers";
import { ctx } from "./main";
import { ISUserInfoRequest } from "./protos/dotnetapp";

const userInfo = ref(ISUserInfoRequest.fromPartial({}));
const userInfoRaw = ref("");

const fetchUserInfoData = async () => {
  if (ctx.value.identityServerKey) {
    try {
      const resp = await fetch(`${window.location.origin}/userinfo`, {
        method: "post",
        body: JSON.stringify(
          ISUserInfoRequest.fromPartial({
            token: ctx.value.identityServerKey.accessToken,
          })
        ),
      });

      if (resp.status !== 200) throw await resp.text();

      userInfoRaw.value = await resp.text();
    } catch (e) {
      return null;
    }
  }
};

fetchUserInfoData();
</script>
<template>
  <div class="m-3">
    <div><h6>User Information</h6></div>
    <div><hr class="border border-dark" /></div>
    <div class="border border-dark shadow shadow-md p-3">
      <pre><small><strong>{{
        (() => {
          try {
            return JSON.stringify(JSON.parse(userInfoRaw), null, 2);
          } catch (e) {
            return "Invalid JSON";
          }
        })()
      }}</strong></small></pre>
    </div>
  </div>
</template>
