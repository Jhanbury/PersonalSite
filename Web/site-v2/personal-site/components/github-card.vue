<template>
  <b-card @click="navigate(repo.html_url)" :title="repo.name" :sub-title="repo.description" bg-variant="dark" text-variant="white" class="tile text-center" >
      <v-container fluid>
        <v-row>
            <v-col>
                <v-icon>mdi-alert-circle-outline</v-icon>
                <p>{{repo.open_issues}}</p>
            </v-col>
            <v-col>
                <v-icon>mdi-star</v-icon>
                <p>{{repo.stargazers_count}}</p>
            </v-col>
            <v-col>
                <v-icon>mdi-source-branch</v-icon>
                <p>{{repo.forks}}</p>
            </v-col>
        </v-row>
      </v-container>
  </b-card>
</template>

<script>
export default {
  props: {
      repo: Object
  },
  methods: {
    navigate(url) {
      window.open(url);
    }
  }
}
</script>

<style>
.tile:hover {
    transform: scale(1.05);
}
</style>