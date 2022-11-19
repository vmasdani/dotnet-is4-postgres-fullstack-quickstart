import { createApp, ref } from "vue";
import App from "./App.vue";
import router from "./router";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import { ISTokenEndpointReturn } from "./protos/dotnetapp";

export const ctx = ref({
  identityServerKey: null as ISTokenEndpointReturn | null,
});

createApp(App).use(router).mount("#app");
