﻿namespace Mindscape.Raygun4Unity.Messages
{
  public class RaygunIdentifierMessage
  {
    public RaygunIdentifierMessage(string user)
    {
      identifier = user;
    }

    /// <summary>
    /// Unique Identifier for this user. Set this to the identifier you use internally to look up users,
    /// or a correlation id for anonymous users if you have one. It doesn't have to be unique, but we will
    /// treat any duplicated values as the same user. If you use the user's email address as the identifier,
    /// enter it here as well as the Email field.
    /// </summary>
    public string identifier { get; set; }

    /// <summary>
    /// Flag indicating whether a user is anonymous or not.
    /// </summary>
    public bool isAnonymous { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    public string email { get; set; }

    /// <summary>
    /// User's full name. If you are going to set any names, you should probably set this one too.
    /// </summary>
    public string fullName { get; set; }

    /// <summary>
    /// User's first name.
    /// </summary>
    public string firstName { get; set; }

    /// <summary>
    /// Device Identifier. Could be used to identify users across apps.
    /// </summary>
    public string uuid { get; set; }
  }
}
