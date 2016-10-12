Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
var divElem = 'AlertDiv';
var messageElem = 'UpdatePanelMessage';
function ToggleAlertDiv(visString)
{
     var adiv = $get(divElem);
     adiv.style.visibility = visString;
     
}
function ClearErrorState() {
     $get(messageElem).innerHTML = '';
     ToggleAlertDiv('hidden');
     
}
function EndRequestHandler(sender, args)
{
   if (args.get_error() != null)
   {
       var errorName = args.get_error().name;
       if (errorName.length > 0 )
       {
          args.set_errorHandled(true);
          ToggleAlertDiv('visible');
          $get(messageElem).innerHTML = 'The panel did not update successfully.';
       }
   }
   
}
