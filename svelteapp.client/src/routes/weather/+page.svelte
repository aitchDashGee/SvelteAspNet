<script>
	async function getWeather() {
		let response = await fetch('weatherforecast');
		let data = await response.json();
		return data;
	}
</script>

<svelte:head>
	<title>Weather</title>
	<meta name="description" content="Gets weather forecast data from ASP.NET API" />
</svelte:head>

<h1>Weather forecast</h1>

{#await getWeather()}
<p>
	<em>
		Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.
	</em>
</p>
{:then forecasts}
<table class="table table-striped" aria-labelledby="tabelLabel">
	<thead>
		<tr>
			<th>Date</th>
			<th>Temp. (C)</th>
			<th>Temp. (F)</th>
			<th>Summary</th>
		</tr>
	</thead>
	<tbody>
		{#each forecasts as forecast}
		<tr key={"forecast.date"}>
			<td>{forecast.date}</td>
			<td>{forecast.temperatureC}</td>
			<td>{forecast.temperatureF}</td>
			<td>{forecast.summary}</td>
		</tr>
		{/each}
	</tbody>
</table>
{/await}