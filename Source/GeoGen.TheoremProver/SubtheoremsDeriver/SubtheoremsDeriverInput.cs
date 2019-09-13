﻿using GeoGen.Constructor;
using GeoGen.Core;
using System;

namespace GeoGen.TheoremProver
{
    /// <summary>
    /// Represents an input of the <see cref="ISubtheoremsDeriver"/>.
    /// </summary>
    public class SubtheoremsDeriverInput
    {
        #region Public properties

        /// <summary>
        /// The picture where we are trying to derive new theorems.
        /// </summary>
        public ContextualPicture ExaminedConfigurationPicture { get; }

        /// <summary>
        /// The list of theorems that are true in the picture.
        /// </summary>
        public TheoremsMap ExaminedConfigurationTheorems { get; }

        /// <summary>
        /// The configuration where our template theorems hold.
        /// </summary>
        public Configuration TemplateConfiguration { get; }

        /// <summary>
        /// The template theorems that are used to derive theorems in our picture.
        /// </summary>
        public TheoremsMap TemplateTheorems { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SubtheoremsDeriverInput"/> class.
        /// </summary>
        /// <param name="examinedConfigurationPicture">The picture where we are trying to derive new theorems.</param>
        /// <param name="examinedConfigurationTheorems">The list of theorems that are true in the picture.</param>
        /// <param name="templateConfiguration">The configuration where our template theorems hold.</param>
        /// <param name="templateTheorems">The template theorems that are used to derive theorems in our picture.</param>
        public SubtheoremsDeriverInput(ContextualPicture examinedConfigurationPicture,
                                       TheoremsMap examinedConfigurationTheorems,
                                       Configuration templateConfiguration,
                                       TheoremsMap templateTheorems)
        {
            ExaminedConfigurationPicture = examinedConfigurationPicture ?? throw new ArgumentNullException(nameof(examinedConfigurationPicture));
            ExaminedConfigurationTheorems = examinedConfigurationTheorems ?? throw new ArgumentNullException(nameof(examinedConfigurationTheorems));
            TemplateConfiguration = templateConfiguration ?? throw new ArgumentNullException(nameof(templateConfiguration));
            TemplateTheorems = templateTheorems ?? throw new ArgumentNullException(nameof(templateTheorems));
        }

        #endregion
    }
}
