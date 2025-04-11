<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-96">
      <h2 class="text-2xl font-semibold text-center mb-6">Sign Up</h2>

      <form @submit.prevent="handleSignUp">
        <div class="mb-4">
          <label for="username" class="block text-sm font-medium text-gray-700">
            Username
          </label>
          <input
            id="username"
            v-model="username"
            type="text"
            placeholder="Enter your username"
            class="w-full p-2 mt-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
        </div>

        <div class="mb-4">
          <label for="password" class="block text-sm font-medium text-gray-700">
            Password
          </label>
          <input
            id="password"
            v-model="password"
            type="password"
            placeholder="Enter your password"
            class="w-full p-2 mt-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
        </div>

        <button
          type="submit"
          class="w-full py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          Sign Up
        </button>
      </form>

      <div v-if="error" class="mt-4 text-red-500 text-center">
        <p>{{ error }}</p>
      </div>

      <div class="mt-4 text-center">
        <p class="text-sm">
          Already have an account?
          <router-link to="/signin" class="text-blue-500">Sign In</router-link>
        </p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { signUpAPI } from "@/services/auth.service";

const username = ref("");
const password = ref("");
const error = ref<string | null>(null);
const router = useRouter();

const handleSignUp = async () => {
  if (!username.value || !password.value) {
    error.value = "Please fill in all fields.";
    return;
  }

  try {
    await signUpAPI({
      username: username.value,
      password: password.value,
    });

    router.push("/signin");
  } catch (err) {
    error.value = "An error occurred during sign up.";
    console.error("Signup error:", err);
  }
};
</script>

<style scoped></style>
