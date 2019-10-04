export const state = () => ({
    repos: [],
    socialLinks: [],
    experience: []
  })
  
export const mutations = {
  add (state, text) {
    state.list.push({
      text: text,
      done: false
    })
  },
  updateRepos (state, repos) {
      state.repos = repos;
  },
  updateSocialLinks (state, links) {
    state.socialLinks = links;
  },
  updateExperience (state, experience) {
    state.experience = experience;
  }
}

export const actions = {
    async getRepos({ commit }){
      const repos = await this.$axios.$get('/api/github-repos');
      commit('updateRepos', repos);
    },
    async getSocialLinks({ commit }){
        const repos = await this.$axios.$get('/api/userinfo/1/socialmediaaccounts');
        commit('updateSocialLinks', repos);
    },
    async getExperience({ commit }){
        const experience = await this.$axios.$get('/api/userinfo/1/experience');
        commit('updateExperience', experience);
    }
}

