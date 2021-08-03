<script type="text/javascript" charset="utf-8">
$(function()
{
// initialise the "Select date" link
$('#date-pick')
	.datePicker(
		// associate the link with a date picker
		{
			createButton:false,
			startDate:'01/01/2011',
			//endDate:'31/12/2020'
		}
	).bind(
		// when the link is clicked display the date picker
		'click',
		function()
		{
			//updateSelects($(this).dpGetSelected()[0]);
			$(this).dpDisplay();
			return false;
		}
	).bind(
		// when a date is selected update the SELECTs
		'dateSelected',
		function(e, selectedDate, $td, state)
		{
			updateSelects(selectedDate);
		}
	).bind(
		'dpClosed',
		function(e, selected)
		{
			updateSelects(selected[0]);
		}
	);
	
function updateSelects(selectedDate)
{
	var selectedDate = new Date(selectedDate);
	var d = selectedDate.getFullYear()+'-'+(selectedDate.getMonth()+parseInt(1))+'-'+selectedDate.getDate();
	document.getElementById('txt_search').value = d; 
	
}
$('#d').trigger('change');
});
</script>

<script type="text/javascript" charset="utf-8">
$(function()
{
// initialise the "Select date" link
$('#date-pick1')
	.datePicker(
		// associate the link with a date picker
		{
			createButton:false,
			startDate:'01/01/2011',
			//endDate:'31/12/2020'
		}
	).bind(
		// when the link is clicked display the date picker
		'click',
		function()
		{
			//updateSelects($(this).dpGetSelected()[0]);
			$(this).dpDisplay();
			return false;
		}
	).bind(
		// when a date is selected update the SELECTs
		'dateSelected',
		function(e, selectedDate, $td, state)
		{
			updateSelects(selectedDate);
		}
	).bind(
		'dpClosed',
		function(e, selected)
		{
			updateSelects(selected[0]);
		}
	);
	
function updateSelects(selectedDate)
{
	var selectedDate = new Date(selectedDate);
	var d = selectedDate.getFullYear()+'-'+(selectedDate.getMonth()+parseInt(1))+'-'+selectedDate.getDate();
	document.getElementById('txt_search1').value = d; 
	
}
$('#d').trigger('change');
});
</script>


