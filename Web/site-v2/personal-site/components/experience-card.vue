<template>
<v-timeline-item>
      
      <template v-slot:icon>
        <v-avatar>
          <img src="http://i.pravatar.cc/64">
        </v-avatar>
      </template>
      <template v-slot:opposite>
        <span v-text="experience.startDate"></span>
      </template>
      <v-card v-show="experience.experienceType === 'Job'" class="elevation-2">
        <v-card-title class="headline">Job</v-card-title>
        <v-card-text>Role</v-card-text> 
      </v-card>
</v-timeline-item>
  
</template>

<script>
export default {
  props: {
      experience: Object
  }  
}
</script>

<style>
.tile:hover {
    transform: scale(1.05);
}
</style>