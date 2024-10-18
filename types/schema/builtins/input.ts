import type { NodeProperties, NodeProperty } from '../properties';
import type { Node } from '../node';
import type { NodeAttribute, NodeAttributes } from '../attributes';

type InputPropertyKeys = 'placeholder' | 'borderColor';
type InputAttributeKeys = 'relatedTag';
type InputProperties = Record<InputPropertyKeys, NodeProperty> & NodeProperties;
type InputAttributes = Record<InputAttributeKeys, NodeAttribute> & NodeAttributes;

export interface Input extends Node {
  properties?: InputProperties;
  attributes?: InputAttributes;
}
