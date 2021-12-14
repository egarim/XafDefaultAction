using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XafDefaultAction.Module.BusinessObjects;

namespace XafDefaultAction.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DefaultActionController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        SimpleAction redirectDetailView;
        public DefaultActionController()
        {
            InitializeComponent();


            TargetObjectType = typeof(DomainObject1);


            redirectDetailView = new SimpleAction(this, "WriteMail", PredefinedCategory.Edit);
            redirectDetailView.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            redirectDetailView.Category = "Hide me))";
            redirectDetailView.Execute += RedirectDetailView_Execute;

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        void RedirectDetailView_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var Os = this.Application.CreateObjectSpace(typeof(DomainObject2));
            
            
            var CurrentObject = this.View.CurrentObject as DomainObject1;
            var CurrentDo2= Os.GetObjectsQuery<DomainObject2>().FirstOrDefault(do2 => do2.Code == CurrentObject.Code);

            var Do2View = this.Application.CreateDetailView(Os, CurrentDo2);
            
            ListViewProcessCurrentObjectController.ShowObject(
              CurrentDo2, e.ShowViewParameters, Application, Frame, Do2View);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var  processCurrentObjectController =
            Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomProcessSelectedItem +=
                    processCurrentObjectController_CustomProcessSelectedItem;
            }

            // Perform various tasks depending on the target View.
        }
        private void processCurrentObjectController_CustomProcessSelectedItem(
        object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            e.Handled = true;
            redirectDetailView.DoExecute();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            var processCurrentObjectController =
           Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomProcessSelectedItem -=
                    processCurrentObjectController_CustomProcessSelectedItem;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
