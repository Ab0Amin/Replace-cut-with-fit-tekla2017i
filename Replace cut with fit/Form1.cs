using System;
using System.Collections;
using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using t3d = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using mui= Tekla.Structures.Model.UI;
using Tekla.Structures.Solid;
using System.Windows.Forms;
using System.Linq;


namespace Replace_cut_with_fit
{
    public partial class Form1 : Form
    {
        Model myModel = new Model();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Part mainPart = new Picker().PickObject(Picker.PickObjectEnum.PICK_ONE_PART) as Part;

                ModelObjectEnumerator objectEnumerator = mainPart.GetChildren();

                while (objectEnumerator.MoveNext())
                {
                    CutPlane cut = objectEnumerator.Current as CutPlane;
                    if (cut != null)
                    {
                        rplaceCutWithFit(cut);
                    }

                }
                myModel.CommitChanges();
            }
            catch (Exception)
            {

                
            }
        }

        private static void rplaceCutWithFit(CutPlane cut)
        {
            Plane cutPlane = cut.Plane;
            ModelObject cutPart = cut.Father;

            Fitting fit = new Fitting();
            fit.Plane = cutPlane;
            fit.Father = cutPart;
            fit.Insert();




            cut.Delete();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                mui.ModelObjectSelector ms = new mui.ModelObjectSelector();
                ModelObjectEnumerator me = ms.GetSelectedObjects();
                while (me.MoveNext())
                {
                    Part part = me.Current as Part;
                    if (part != null)
                    {


                        ModelObjectEnumerator objectEnumerator = part.GetChildren();

                        while (objectEnumerator.MoveNext())
                        {
                            CutPlane cut = objectEnumerator.Current as CutPlane;
                            if (cut != null)
                            {
                                rplaceCutWithFit(cut);
                            }

                        }
                        myModel.CommitChanges();

                    }


                }
            }
            catch (Exception)
            {

            }
            
        }
    }
}
