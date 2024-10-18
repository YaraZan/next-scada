import type { NodeProperties, NodeProperty } from '../properties';
import type { Node } from '../node';
import type { NodeAttribute, NodeAttributes } from '../attributes';

type LabelPropertyKeys = 'fontSize' | 'fontWeight' | 'color';
type LabelAttributeKeys = 'textContent';
type LabelProperties = Record<LabelPropertyKeys, NodeProperty> & NodeProperties;
type LabelAttributes = Record<LabelAttributeKeys, NodeAttribute> & NodeAttributes;

export interface Label extends Node {
  properties?: LabelProperties;
  attributes?: LabelAttributes;
}
