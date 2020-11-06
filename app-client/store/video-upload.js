const  initState = () => ({
  uploadPromise: null,
  active: false,
  component: null
})

export  const  state = initState
export  const  mutations = {
  activate(state, {component}){
    state.active = true;
    state.component = component;
  },
  hide(state){
    state.active = false;
  },
  setUploadPromise(state, {uploadPromise}){
    state.uploadPromise = uploadPromise;
  },
  reset(state){
    Object.assign(state,initState())
  }
}

export  const  actions = {
  async startVideoUpload({commit, dispatch},{form}){
    const uploadPromise = await this.$axios.$post("/api/videos", form);
    commit("setUploadPromise",{uploadPromise});
  },
  async createSubmission({state, commit, dispatch},{form}){
    if (!state.uploadPromise) {
      console.log("Upload Video Null");
      return
    }

    form.video = await state.uploadPromise;
    await dispatch('submission/createSubmission',{form},{root:true});
    commit('reset');
  }
}
