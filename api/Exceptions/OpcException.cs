using System;

namespace api.Exceptions
{
  public class OpcBrowsingException : Exception
  {
    public OpcBrowsingException(string errors)
        : base("An error occurred while browsing OPC servers.;" + errors)
    {
    }
  }

  public class OpcItemsBrowsingException : Exception
  {
    public OpcItemsBrowsingException()
        : base("An error occurred while browsing OPC server items.;")
    {
    }
  }

  public class OpcItemCreationException : Exception
  {
    public OpcItemCreationException()
        : base("An error occurred while creating the OPC item.")
    {
    }
  }

  public class OpcItemReadingException : Exception
  {
    public OpcItemReadingException()
        : base("An error occurred while reading the OPC item.")
    {
    }
  }

  public class OpcItemSubscriptionException : Exception
  {
    public OpcItemSubscriptionException()
        : base("An error occurred while subcribing the OPC item.")
    {
    }
  }

  public class OpcItemUnsubscriptionException : Exception
  {
    public OpcItemUnsubscriptionException()
        : base("An error occurred while unsubcribing of OPC items.")
    {
    }
  }

  public class OpcServerExistsCheckException : Exception
  {
    public OpcServerExistsCheckException()
        : base("An error occurred trying to check OPC server availability.")
    {
    }
  }
}
