namespace EpubCore.Cli.ActionHandlers;

public enum GetEpubFilterKey
{
    /// <summary>
    /// The unique identifier of this EPub
    /// </summary>
    UniqueIdentifier,
    /// <summary>
    /// EPub version
    /// </summary>
    Version,
    /// <summary>
    /// Authors
    /// </summary>
    Authors,
    /// <summary>
    /// Publishers
    /// </summary>
    Publishers,
    /// <summary>
    /// Contributors
    /// </summary>
    Contributors,
    /// <summary>
    /// Titles for this EPub
    /// </summary>
    Titles,
    /// <summary>
    /// Table of Contents
    /// </summary>
    Toc,
    /// <summary>
    /// Css Stylesheets
    /// </summary>
    Css,
    /// <summary>
    /// Details about the EPub Cover
    /// </summary>
    Cover,
    /// <summary>
    /// List of all images
    /// </summary>
    Images, 
    /// <summary>
    /// List of all fonts
    /// </summary>
    Fonts,
    /// <summary>
    /// List of all HTML files
    /// </summary>
    Html
}