/* eslint-disable */
import Long from "long";
import _m0 from "protobufjs/minimal";

export const protobufPackage = "";

export interface ISLoginError {
  errorDescription?: string | undefined;
}

export interface ISTokenEndpointReturn {
  accessToken?: string | undefined;
  expiresIn?: number | undefined;
  tokenType?: string | undefined;
  scope?: string | undefined;
}

export interface ISUserInfoRequest {
  token?: string | undefined;
}

export interface ISUserInfoResponse {
}

function createBaseISLoginError(): ISLoginError {
  return { errorDescription: undefined };
}

export const ISLoginError = {
  encode(message: ISLoginError, writer: _m0.Writer = _m0.Writer.create()): _m0.Writer {
    if (message.errorDescription !== undefined) {
      writer.uint32(10).string(message.errorDescription);
    }
    return writer;
  },

  decode(input: _m0.Reader | Uint8Array, length?: number): ISLoginError {
    const reader = input instanceof _m0.Reader ? input : new _m0.Reader(input);
    let end = length === undefined ? reader.len : reader.pos + length;
    const message = createBaseISLoginError();
    while (reader.pos < end) {
      const tag = reader.uint32();
      switch (tag >>> 3) {
        case 1:
          message.errorDescription = reader.string();
          break;
        default:
          reader.skipType(tag & 7);
          break;
      }
    }
    return message;
  },

  fromJSON(object: any): ISLoginError {
    return { errorDescription: isSet(object.errorDescription) ? String(object.errorDescription) : undefined };
  },

  toJSON(message: ISLoginError): unknown {
    const obj: any = {};
    message.errorDescription !== undefined && (obj.errorDescription = message.errorDescription);
    return obj;
  },

  fromPartial<I extends Exact<DeepPartial<ISLoginError>, I>>(object: I): ISLoginError {
    const message = createBaseISLoginError();
    message.errorDescription = object.errorDescription ?? undefined;
    return message;
  },
};

function createBaseISTokenEndpointReturn(): ISTokenEndpointReturn {
  return { accessToken: undefined, expiresIn: undefined, tokenType: undefined, scope: undefined };
}

export const ISTokenEndpointReturn = {
  encode(message: ISTokenEndpointReturn, writer: _m0.Writer = _m0.Writer.create()): _m0.Writer {
    if (message.accessToken !== undefined) {
      writer.uint32(10).string(message.accessToken);
    }
    if (message.expiresIn !== undefined) {
      writer.uint32(16).int64(message.expiresIn);
    }
    if (message.tokenType !== undefined) {
      writer.uint32(26).string(message.tokenType);
    }
    if (message.scope !== undefined) {
      writer.uint32(34).string(message.scope);
    }
    return writer;
  },

  decode(input: _m0.Reader | Uint8Array, length?: number): ISTokenEndpointReturn {
    const reader = input instanceof _m0.Reader ? input : new _m0.Reader(input);
    let end = length === undefined ? reader.len : reader.pos + length;
    const message = createBaseISTokenEndpointReturn();
    while (reader.pos < end) {
      const tag = reader.uint32();
      switch (tag >>> 3) {
        case 1:
          message.accessToken = reader.string();
          break;
        case 2:
          message.expiresIn = longToNumber(reader.int64() as Long);
          break;
        case 3:
          message.tokenType = reader.string();
          break;
        case 4:
          message.scope = reader.string();
          break;
        default:
          reader.skipType(tag & 7);
          break;
      }
    }
    return message;
  },

  fromJSON(object: any): ISTokenEndpointReturn {
    return {
      accessToken: isSet(object.accessToken) ? String(object.accessToken) : undefined,
      expiresIn: isSet(object.expiresIn) ? Number(object.expiresIn) : undefined,
      tokenType: isSet(object.tokenType) ? String(object.tokenType) : undefined,
      scope: isSet(object.scope) ? String(object.scope) : undefined,
    };
  },

  toJSON(message: ISTokenEndpointReturn): unknown {
    const obj: any = {};
    message.accessToken !== undefined && (obj.accessToken = message.accessToken);
    message.expiresIn !== undefined && (obj.expiresIn = Math.round(message.expiresIn));
    message.tokenType !== undefined && (obj.tokenType = message.tokenType);
    message.scope !== undefined && (obj.scope = message.scope);
    return obj;
  },

  fromPartial<I extends Exact<DeepPartial<ISTokenEndpointReturn>, I>>(object: I): ISTokenEndpointReturn {
    const message = createBaseISTokenEndpointReturn();
    message.accessToken = object.accessToken ?? undefined;
    message.expiresIn = object.expiresIn ?? undefined;
    message.tokenType = object.tokenType ?? undefined;
    message.scope = object.scope ?? undefined;
    return message;
  },
};

function createBaseISUserInfoRequest(): ISUserInfoRequest {
  return { token: undefined };
}

export const ISUserInfoRequest = {
  encode(message: ISUserInfoRequest, writer: _m0.Writer = _m0.Writer.create()): _m0.Writer {
    if (message.token !== undefined) {
      writer.uint32(10).string(message.token);
    }
    return writer;
  },

  decode(input: _m0.Reader | Uint8Array, length?: number): ISUserInfoRequest {
    const reader = input instanceof _m0.Reader ? input : new _m0.Reader(input);
    let end = length === undefined ? reader.len : reader.pos + length;
    const message = createBaseISUserInfoRequest();
    while (reader.pos < end) {
      const tag = reader.uint32();
      switch (tag >>> 3) {
        case 1:
          message.token = reader.string();
          break;
        default:
          reader.skipType(tag & 7);
          break;
      }
    }
    return message;
  },

  fromJSON(object: any): ISUserInfoRequest {
    return { token: isSet(object.token) ? String(object.token) : undefined };
  },

  toJSON(message: ISUserInfoRequest): unknown {
    const obj: any = {};
    message.token !== undefined && (obj.token = message.token);
    return obj;
  },

  fromPartial<I extends Exact<DeepPartial<ISUserInfoRequest>, I>>(object: I): ISUserInfoRequest {
    const message = createBaseISUserInfoRequest();
    message.token = object.token ?? undefined;
    return message;
  },
};

function createBaseISUserInfoResponse(): ISUserInfoResponse {
  return {};
}

export const ISUserInfoResponse = {
  encode(_: ISUserInfoResponse, writer: _m0.Writer = _m0.Writer.create()): _m0.Writer {
    return writer;
  },

  decode(input: _m0.Reader | Uint8Array, length?: number): ISUserInfoResponse {
    const reader = input instanceof _m0.Reader ? input : new _m0.Reader(input);
    let end = length === undefined ? reader.len : reader.pos + length;
    const message = createBaseISUserInfoResponse();
    while (reader.pos < end) {
      const tag = reader.uint32();
      switch (tag >>> 3) {
        default:
          reader.skipType(tag & 7);
          break;
      }
    }
    return message;
  },

  fromJSON(_: any): ISUserInfoResponse {
    return {};
  },

  toJSON(_: ISUserInfoResponse): unknown {
    const obj: any = {};
    return obj;
  },

  fromPartial<I extends Exact<DeepPartial<ISUserInfoResponse>, I>>(_: I): ISUserInfoResponse {
    const message = createBaseISUserInfoResponse();
    return message;
  },
};

declare var self: any | undefined;
declare var window: any | undefined;
declare var global: any | undefined;
var globalThis: any = (() => {
  if (typeof globalThis !== "undefined") {
    return globalThis;
  }
  if (typeof self !== "undefined") {
    return self;
  }
  if (typeof window !== "undefined") {
    return window;
  }
  if (typeof global !== "undefined") {
    return global;
  }
  throw "Unable to locate global object";
})();

type Builtin = Date | Function | Uint8Array | string | number | boolean | undefined;

export type DeepPartial<T> = T extends Builtin ? T
  : T extends Array<infer U> ? Array<DeepPartial<U>> : T extends ReadonlyArray<infer U> ? ReadonlyArray<DeepPartial<U>>
  : T extends {} ? { [K in keyof T]?: DeepPartial<T[K]> }
  : Partial<T>;

type KeysOfUnion<T> = T extends T ? keyof T : never;
export type Exact<P, I extends P> = P extends Builtin ? P
  : P & { [K in keyof P]: Exact<P[K], I[K]> } & { [K in Exclude<keyof I, KeysOfUnion<P>>]: never };

function longToNumber(long: Long): number {
  if (long.gt(Number.MAX_SAFE_INTEGER)) {
    throw new globalThis.Error("Value is larger than Number.MAX_SAFE_INTEGER");
  }
  return long.toNumber();
}

if (_m0.util.Long !== Long) {
  _m0.util.Long = Long as any;
  _m0.configure();
}

function isSet(value: any): boolean {
  return value !== null && value !== undefined;
}
