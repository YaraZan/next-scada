export interface OpcServer {
  name: string,
  url: string,
  connectionString: string,
  host?: string|null,
  protocol: OpcServerProtocol
}

export type OpcServerProtocol = 'DA' | 'UA' | 'AC'; // Add alarms and conditions and so on...

export interface OpcServerNode {
  id: string,
  name: string,
  type: OpcServerNodeType
}

export type OpcServerNodeType = 'FOLDER' | 'TAG';
