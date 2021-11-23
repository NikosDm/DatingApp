export interface Group {
  group: string;
  connections: Connection[];
}

interface Connection {
  connectionId: string;
  username: string;
}
