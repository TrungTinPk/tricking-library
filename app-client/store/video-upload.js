﻿const  initState = () => ({
  uploadPromise: null,
  uploadCompleted : null,
  uploadCancelSource: null,
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
  setTask(state, {uploadPromise,source}){
    state.uploadPromise = uploadPromise;
    state.uploadCancelSource = source;
  },
  completeUpload(state){
    state.uploadCompleted = true;
  },
  reset(state){
    Object.assign(state,initState())
  }
}

export  const  actions = {
  async startVideoUpload({commit, dispatch},{form}){
    const source = this.$axios.CancelToken.source();
    const uploadPromise = await this.$axios.$post("/api/videos", form, {
      progress: false,
      cancelToken: source.token
    }).then(video => {
      commit('completeUpload');
      return video;
    }).catch(err => {
      if (this.$axios.isCancel(err)){

      }
    });
    commit("setTask",{uploadPromise,source});
  },
  async cancelUpload({state, commit}) {
    if (state.uploadPromise){
      if (state.uploadCompleted){
        commit('hide');
        const video = await  state.uploadPromise;
        await this.$axios.$delete("/api/videos" + video);
      } else {
        state.uploadCancelSource.cancel();
      }
    }
    commit('reset');
  },
  async createSubmission({state, commit, dispatch},{form}){
    if (!state.uploadPromise) {
      console.log("Upload Video Null");
      return
    }

    form.video = await state.uploadPromise;
    await dispatch('submissions/createSubmission',{form},{root:true});
    commit('reset');
  }
}
