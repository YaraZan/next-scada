import type { NodeProperties } from './properties';

/**
 * Base schema node type
 */
export interface SchemaNode {
  uuid: string,
  name: string;
  type: NodeType;
  properties?: NodeProperties | null;
  position?:  NodePosition | null;
}

/**
 * Node position
 *
 * Stores node element adjustment data
 */
type NodePosition = {
  x: number;
  y: number;
  z: number;
};

/**
 * Node types
 */
export type NodeType =
  | 'Text'
  | 'Image'
  | 'Input'
  | 'Checkbox'
  | 'Radio';
