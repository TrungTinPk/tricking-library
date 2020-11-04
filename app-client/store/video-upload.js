const  initState = () => ({
  uploadPromise: null,
  active: false,
  type: "",
  step: 1
})

export  const  state = initState
export  const  mutations = {
  toggleActivity(state){
    state.active = !state.status;
    if (!state.active){
      Object.assign(state,initState())
    }
  },
  setType(state, {type}){
    state.type = type;
    state.step++;
  },
  setUploadPromise(state, {uploadPromise}){
    state.uploadPromise = uploadPromise;
    state.step++;
  },
  reset(state){
    Object.assign(state,initState())
  }
}

export  const  actions = {
  async startVideoUpload({commit, dispatch},{form}){
    const uploadPromise = await this.$axios.$post("/api/tricks", form);
    commit("setUploadPromise",uploadPromise);
  },
  async creatTrick({commit, dispatch},{trick}){
    await this.$axios.$post("/api/tricks", trick);
    await dispatch("tricks/fetchTricks");
  }
}
