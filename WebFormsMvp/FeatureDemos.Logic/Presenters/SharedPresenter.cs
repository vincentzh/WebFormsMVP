﻿using System;
using WebFormsMvp.FeatureDemos.Logic.Views.Models;

namespace WebFormsMvp.FeatureDemos.Logic.Presenters
{
    public class SharedPresenter
        : Presenter<IView<SharedPresenterViewModel>>
    {
        public SharedPresenter(IView<SharedPresenterViewModel> view)
            : base(view)
        {
            View.Load += Load;
        }

        void Load(object sender, EventArgs e)
        {
            View.Model.Message = string.Format(@"Presenter instance: {0}", Guid.NewGuid());
        }
    }
}