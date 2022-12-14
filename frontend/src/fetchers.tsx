export const fetchUsers = async (params: { apiKey?: string | null }) => {
  try {
    const resp = await fetch(`${window.location.origin}/users`, {
      headers: { Authorization: `Bearer ${params.apiKey}` },
    });

    if (resp.status !== 200) {
      throw await resp.text();
    }

    return await resp.json();
  } catch (e) {
    console.error(e);
    return null;
  }
};
