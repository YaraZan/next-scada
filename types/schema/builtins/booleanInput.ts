import type { Node, NodeProperty, NodeProperties } from '../node';

type BooleanPropertyKeys = 'source';
type BooleanProperties = Record<BooleanPropertyKeys, NodeProperty> & NodeProperties;

export interface BooleanInput extends Node {
  properties?: BooleanProperties;
}
