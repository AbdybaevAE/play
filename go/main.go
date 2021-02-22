package main

import (
	"context"
	mhttp "github.com/unistack-org/micro-client-http/v3"
	jsoncodec "github.com/unistack-org/micro-codec-json/v3"
	httpsrv "github.com/unistack-org/micro-server-http/v3"
	pb "github.com/unistack-org/micro-tests/client/http/proto"
	"github.com/unistack-org/micro-tests/server/http/handler"
	"github.com/unistack-org/micro/v3"
	"github.com/unistack-org/micro/v3/client"
	"github.com/unistack-org/micro/v3/logger"
	"github.com/unistack-org/micro/v3/server"
)

func main() {
	ctx, cancel := context.WithCancel(context.Background())
	defer cancel()

	/*
	   Options for service, pass the context with cancel
	*/
	options := append([]micro.Option{},
		micro.Server(httpsrv.NewServer(
			server.Name("github-service"),
			server.Version("1.0"),
			server.Address(":8080"),
			server.Context(ctx),
			server.Codec("application/json", jsoncodec.NewCodec()),
		)),
		micro.Client(mhttp.NewClient(
			client.Name("github-client"),
			client.Context(ctx),
			client.Codec("application/json", jsoncodec.NewCodec()),
			client.ContentType("application/json"),
		)),
		micro.Context(ctx),
	)
	// create new service
	srv := micro.NewService(options...)

	// init all stuff
	if err := srv.Init(); err != nil {
		logger.Fatal(ctx, err)
	}
	// create handler
	eh := handler.NewGithubHandler()
	// register handler in server
	if err := pb.RegisterGithubHandler(srv.Server(), eh); err != nil {
		logger.Fatal(ctx, err)
	}
	// run service
	if err := srv.Run(); err != nil {
		logger.Fatal(ctx, err)
	}
}
