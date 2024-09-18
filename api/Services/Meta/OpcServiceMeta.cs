using api.Models.Opc;

namespace api.Services.Meta
{
  /// <summary>
  /// Opc meta class.
  /// </summary>
  public abstract class OpcServiceMeta
  {
    /// <summary>
    /// Browses for available OPC Data Access (DA) protocol servers, either locally or on a remote host.
    /// </summary>
    /// <param name="host">
    /// The host address (hostname or IP) where the OPC DA servers are expected to be located.
    /// If <c>null</c> or empty, the method will browse for OPC DA servers on the local machine.
    /// </param>
    /// <returns>
    /// An array of <see cref="OpcDa"/> objects representing the available OPC DA servers on the specified host (or locally if no host is provided).
    /// </returns>
    public abstract OpcDa[] BrowseDaServers(string? host = null);

    /// <summary>
    /// Browses for available OPC Unified Architecture (UA) protocol servers, either locally or on a remote host.
    /// </summary>
    /// <param name="host">
    /// The host address (hostname or IP) where the OPC UA servers are expected to be located.
    /// If <c>null</c> or empty, the method will browse for OPC UA servers on the local machine.
    /// </param>
    /// <returns>
    /// An array of <see cref="OpcUa"/> objects representing the available OPC UA servers on the specified host (or locally if no host is provided).
    /// </returns>
    public abstract OpcUa[] BrowseUaServers(string? host = null);


    /// <summary>
    /// Checks if a specified OPC server exists either locally or on a remote host.
    /// </summary>
    /// <param name="opcServer">An <see cref="OpcServer"/> object representing the server to check for existence.</param>
    /// <param name="host">
    /// The host address (hostname or IP) where the OPC server is expected to be located.
    /// If <c>null</c> or empty, the method will check locally for the OPC server.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified OPC server exists on the given host (or locally if no host is provided); otherwise, <c>false</c>.
    /// </returns>
    public abstract bool ServerExists(OpcServer opcServer, string? host = null);

    /// <summary>
    /// Browse available OPC servers (both UA and optionally DA) from a specified host or locally.
    /// </summary>
    /// <param name="withDa">Specifies whether to include OPC DA protocol servers in the search. If set to <c>true</c>, both UA and DA servers are browsed; otherwise, only UA servers are browsed.</param>
    /// <param name="host">The hostname or IP address of the remote machine to browse for servers. If <c>null</c> or empty, the method browses for local servers.</param>
    /// <returns>An array of <see cref="OpcServer"/> objects representing the available OPC servers.</returns>
    public abstract OpcServer[] BrowseServers(bool withDa = false, string? host = null);

  }
}
