import { ISUserInfoRequest, ISUserInfoResponse } from "./protos/dotnetapp";

export const fetchUserInfoProto = async (params: {
  accessToken: string | undefined;
}) => {
  try {
    const resp = await fetch(`${window.location.origin}/userinfo`, {
      method: "post", 
      body: JSON.stringify(
        ISUserInfoRequest.fromPartial({
          token: params.accessToken,
        })
      ),
    });

    if (resp.status !== 200) throw await resp.text();

    return ISUserInfoResponse.fromPartial({});
  } catch (e) {
    return null;
  }
};
