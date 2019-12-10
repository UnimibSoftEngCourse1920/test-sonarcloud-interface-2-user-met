﻿using System;

namespace SteveCadwallader.CodeMaid.IntegrationTests.Reorganizing.Data
{
    public class RegionsRemoveAndInsertWithoutAccessModifiers
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionsRemoveAndInsertWithoutAccessModifiers" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        internal RegionsRemoveAndInsertWithoutAccessModifiers(CodeMaidPackage package)
        {
            #region RegionsInMethodsShouldBeIgnored

            Package = package;

            StartTracking();

            #endregion RegionsInMethodsShouldBeIgnored
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the hosting package.
        /// </summary>
        private CodeMaidPackage Package { get; set; }

        /// <summary>
        /// Gets or sets the active document.
        /// </summary>
        private Document TrackedDocument { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            RestoreTrackedDocument();
        }

        /// <summary>
        /// Restores the tracked document if not already active.
        /// </summary>
        internal void RestoreTrackedDocument()
        {
            if (TrackedDocument != null && Package.ActiveDocument != TrackedDocument)
            {
                TrackedDocument.Activate();
            }
        }

        /// <summary>
        /// Starts tracking the active document.
        /// </summary>
        internal void StartTracking()
        {
            // Cache the active document.
            TrackedDocument = Package.ActiveDocument;
        }

        #endregion Methods
    }
}