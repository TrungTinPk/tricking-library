const  initState = () => ({})

export  const  state = initState
export  const  mutations = {
  reset(state){
    Object.assign(state,initState())
  }
}

export  const  actions = {
  async nuxtServerInit({commit, dispatch}){
    const message = await this.$axios.$get("http://localhost:5000/api/home");
    await dispatch("tricks/fetchTricks");
  }
}
