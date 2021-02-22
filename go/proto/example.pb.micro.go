// Code generated by protoc-gen-micro. DO NOT EDIT.
// source: example.proto

package pb

import (
	fmt "fmt"
	proto "github.com/golang/protobuf/proto"
	_ "github.com/grpc-ecosystem/grpc-gateway/v2/protoc-gen-openapiv2/options"
	_ "google.golang.org/genproto/googleapis/api/annotations"
	math "math"
)

import (
	context "context"
	api "github.com/micro/go-micro/v2/api"
	client "github.com/micro/go-micro/v2/client"
	server "github.com/micro/go-micro/v2/server"
)

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = fmt.Errorf
var _ = math.Inf

// This is a compile-time assertion to ensure that this generated file
// is compatible with the proto package it is being compiled against.
// A compilation error at this line likely means your copy of the
// proto package needs to be updated.
const _ = proto.ProtoPackageIsVersion3 // please upgrade the proto package

// Reference imports to suppress errors if they are not otherwise used.
var _ api.Endpoint
var _ context.Context
var _ client.Option
var _ server.Option

// Api Endpoints for Github service

func NewGithubEndpoints() []*api.Endpoint {
	return []*api.Endpoint{
		&api.Endpoint{
			Name:    "Github.LookupUser",
			Path:    []string{"/users/{username}"},
			Method:  []string{"GET"},
			Handler: "rpc",
		},
	}
}

// Client API for Github service

type GithubService interface {
	LookupUser(ctx context.Context, in *LookupUserReq, opts ...client.CallOption) (*LookupUserRsp, error)
}

type githubService struct {
	c    client.Client
	name string
}

func NewGithubService(name string, c client.Client) GithubService {
	return &githubService{
		c:    c,
		name: name,
	}
}

func (c *githubService) LookupUser(ctx context.Context, in *LookupUserReq, opts ...client.CallOption) (*LookupUserRsp, error) {
	req := c.c.NewRequest(c.name, "Github.LookupUser", in)
	out := new(LookupUserRsp)
	err := c.c.Call(ctx, req, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// Server API for Github service

type GithubHandler interface {
	LookupUser(context.Context, *LookupUserReq, *LookupUserRsp) error
}

func RegisterGithubHandler(s server.Server, hdlr GithubHandler, opts ...server.HandlerOption) error {
	type github interface {
		LookupUser(ctx context.Context, in *LookupUserReq, out *LookupUserRsp) error
	}
	type Github struct {
		github
	}
	h := &githubHandler{hdlr}
	opts = append(opts, api.WithEndpoint(&api.Endpoint{
		Name:    "Github.LookupUser",
		Path:    []string{"/users/{username}"},
		Method:  []string{"GET"},
		Handler: "rpc",
	}))
	return s.Handle(s.NewHandler(&Github{h}, opts...))
}

type githubHandler struct {
	GithubHandler
}

func (h *githubHandler) LookupUser(ctx context.Context, in *LookupUserReq, out *LookupUserRsp) error {
	return h.GithubHandler.LookupUser(ctx, in, out)
}
