export const state = () => ({
    repos: [],
    socialLinks: []
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
      }
}

