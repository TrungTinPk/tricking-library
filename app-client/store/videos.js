const  initState = () => ({
  uploadPromise: null
})

export  const  state = initState
export  const  mutations = {
  setUploadPromise(state, {uploadPromise}){
    state.uploadPromise = uploadPromise;
  },
  reset(state){
    Object.assign(state,initState())
  }
}

export  const  actions = {
  async startVideoUpload({commit, dispatch},{form}){
    const uploadPromise = await this.$axios.$post("http://localhost:5000/api/tricks", form);
    commit("setUploadPromise",uploadPromise);
  }
}
