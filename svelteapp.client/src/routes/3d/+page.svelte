<script>
import App from './App.svelte'
</script>

<svelte:head>
	<title>3D</title>
	<meta name="description" content="3D using Threlte and Three.js" />
</svelte:head>

<h1>
    3D
</h1>

<div id="root">
  <App />
</div>

<style>
  :global(body) {
    margin: 0;
  }

  div {
    width: 50vw;
    height: 50vh;
    background: rgb(13, 19, 32);
    background: linear-gradient(180deg, rgba(13, 19, 32, 1) 0%, rgba(8, 12, 21, 1) 100%);
  }
</style>
