<script setup lang="ts">
import { fetchUsers } from "@/fetchers";
import { ctx } from "@/main";
import { ref } from "vue";

let users = ref([]);

const fetchUsersData = async () => {
  users.value = await fetchUsers({
    apiKey: ctx.value.identityServerKey?.accessToken ?? "",
  });
};

const handleInit = async () => {
  fetchUsersData();
};

handleInit();
</script>

<template>
  <div class="container">
    <div>
      <h4>Users</h4>
    </div>
    <hr class="border border-dark" />
    <div
      class="overflow-auto border border-dark shadow shadow-md bg-light"
      style="height: 75vh"
    >
      <table
        class="table table-sm table-hover"
        style="border-collapse: separate"
      >
        <tr>
          <th class="bg-dark text-light" v-for="h in ['#', 'ID', 'Name']">
            {{ h }}
          </th>
        </tr>
        <tr v-for="(u, i) of (users as any)?.users ?? []">
          <td class="border border-dark">{{ i + 1 }}</td>
          <td class="border border-dark">{{ (u as any)?.id }}</td>
          <td class="border border-dark">{{ (u as any)?.userName }}</td>
        </tr>
      </table>
    </div>
  </div>
</template>
