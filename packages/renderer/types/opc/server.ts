export interface Server {
  name: string;
  url: string;
  connectionString: string;
  host?: string | null;
  protocol: ServerProtocol;
}

export type ServerProtocol = 'DA' | 'UA' | 'AC'; // Add alarms and conditions and so on...

export interface ServerNode {
  id: string;
  name: string;
  type: ServerNodeType;
}

export type ServerNodeType = 'FOLDER' | 'TAG';
